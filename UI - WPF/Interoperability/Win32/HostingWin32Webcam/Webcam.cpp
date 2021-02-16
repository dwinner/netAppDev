#include "stdafx.h"
#include "Webcam.h"

#include <d3d9.h>
#include <dshow.h>
#include <vmr9.h>

int _width = 0;
int _height = 0;
IGraphBuilder* _graphBuilder = nullptr;
IMediaControl* _mediaControl = nullptr;
IVMRWindowlessControl* _windowlessControl = nullptr;
IBaseFilter* _baseFilter = nullptr;
IPin* _pin = nullptr;
bool _initialized = false;
HWND _hwnd = nullptr;

LRESULT WINAPI WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
   switch (msg)
   {
   case WM_ERASEBKGND:
      DefWindowProc(hwnd, msg, wParam, lParam);
      Webcam::Repaint();
      break;
   default:
      return DefWindowProc(hwnd, msg, wParam, lParam);
   }
   return 0;
}

void Webcam::FreeMedia(AM_MEDIA_TYPE* type)
{
   if (type == nullptr)
      return;

   if (type->cbFormat != NULL)
      CoTaskMemFree(static_cast<PVOID>(type->pbFormat));

   if (type->pUnk != nullptr)
      type->pUnk->Release();

   CoTaskMemFree(static_cast<PVOID>(type));
}

void Webcam::SetupWindow()
{
   WNDCLASS wc;
   wc.style = CS_VREDRAW | CS_HREDRAW;
   wc.lpfnWndProc = WndProc;
   wc.cbClsExtra = 0;
   wc.cbWndExtra = 0;
   wc.hInstance = GetModuleHandle(nullptr);
   wc.hIcon = LoadIcon(nullptr, IDI_APPLICATION);
   wc.hCursor = LoadCursor(nullptr, IDC_ARROW);
   wc.hbrBackground = reinterpret_cast<HBRUSH>(COLOR_SCROLLBAR + 1);
   wc.lpszMenuName = nullptr;
   wc.lpszClassName = L"WebcamClass";

   RegisterClass(&wc);
}

HRESULT Webcam::Initialize(int width, int height)
{
   _width = width;
   _height = height;

   // Create and register the Window Class
   SetupWindow();

   auto hr = CoCreateInstance(CLSID_FilterGraph, nullptr, CLSCTX_INPROC_SERVER, IID_IGraphBuilder, reinterpret_cast<void **>(&_graphBuilder));

   if (FAILED(hr))
      return hr;

   IBaseFilter* pBaseFilter = nullptr;
   ICreateDevEnum* pCreateDevEnum = nullptr;
   IEnumMediaTypes* pEnumMediaTypes = nullptr;
   IEnumMoniker* pEnumMoniker = nullptr;
   IEnumPins* pEnumPins = nullptr;
   IVMRFilterConfig* pFilterConfig = nullptr;
   IMoniker* pMoniker = nullptr;
   IAMStreamConfig* pStreamConfig = nullptr;

   __try
   {
      hr = CoCreateInstance(CLSID_VideoMixingRenderer, nullptr, CLSCTX_INPROC, IID_IBaseFilter, reinterpret_cast<void**>(&pBaseFilter));
      if (FAILED(hr))
         return hr;

      hr = _graphBuilder->AddFilter(pBaseFilter, L"Video Mixing Renderer");
      if (FAILED(hr))
         return hr;

      hr = pBaseFilter->QueryInterface(IID_IVMRFilterConfig, reinterpret_cast<void**>(&pFilterConfig));
      if (FAILED(hr))
         return hr;

      hr = pFilterConfig->SetRenderingMode(VMRMode_Windowless);
      if (FAILED(hr))
         return hr;

      hr = pBaseFilter->QueryInterface(IID_IVMRWindowlessControl, reinterpret_cast<void**>(&_windowlessControl));
      if (FAILED(hr))
         return hr;

      hr = CoCreateInstance(CLSID_SystemDeviceEnum, nullptr, CLSCTX_INPROC_SERVER, IID_ICreateDevEnum, reinterpret_cast<void**>(&pCreateDevEnum));
      if (FAILED(hr))
         return hr;

      hr = pCreateDevEnum->CreateClassEnumerator(CLSID_VideoInputDeviceCategory, &pEnumMoniker, 0);
      if (hr != NOERROR)
      {
         // No video capture device could be found.
         return E_FAIL;
      }

      // Just grab the first device:
      ULONG fetched;
      hr = pEnumMoniker->Next(1, &pMoniker, &fetched);
      if (FAILED(hr))
         return hr;

      hr = pMoniker->BindToObject(nullptr, nullptr, IID_IBaseFilter, reinterpret_cast<void**>(&_baseFilter));
      if (FAILED(hr))
         return hr;

      hr = _graphBuilder->AddFilter(_baseFilter, L"Video Capture");
      if (FAILED(hr))
         return hr;

      hr = _baseFilter->EnumPins(&pEnumPins);
      if (FAILED(hr))
         return hr;

      hr = pEnumPins->Reset();
      if (FAILED(hr))
         return hr;

      hr = pEnumPins->Next(1, &_pin, nullptr);
      if (FAILED(hr))
         return hr;

      hr = _graphBuilder->QueryInterface(IID_IMediaControl, reinterpret_cast<void **>(&_mediaControl));
      if (FAILED(hr))
         return hr;

      AM_MEDIA_TYPE* type = nullptr;

      hr = _pin->EnumMediaTypes(&pEnumMediaTypes);
      if (SUCCEEDED(hr))
      {
         while (pEnumMediaTypes->Next(1, &type, nullptr) == S_OK)
         {
            if (type->formattype == FORMAT_VideoInfo)
            {
               auto videoInfoHeader = reinterpret_cast<VIDEOINFOHEADER *>(type->pbFormat);

               if (videoInfoHeader->bmiHeader.biWidth == width && videoInfoHeader->bmiHeader.biHeight == height)
               {
                  hr = _pin->QueryInterface(IID_IAMStreamConfig, reinterpret_cast<void **>(&pStreamConfig));
                  if (SUCCEEDED(hr) && type != nullptr)
                  {
                     hr = pStreamConfig->SetFormat(type);
                     FreeMedia(type);
                  }

                  break;
               }

               FreeMedia(type);
            }
         }
      }

      _initialized = true;
   }
   __finally
   {
      if (pBaseFilter)
         pBaseFilter->Release();
      if (pCreateDevEnum)
         pCreateDevEnum->Release();
      if (pEnumMediaTypes)
         pEnumMediaTypes->Release();
      if (pEnumMoniker)
         pEnumMoniker->Release();
      if (pEnumPins)
         pEnumPins->Release();
      if (pFilterConfig)
         pFilterConfig->Release();
      if (pMoniker)
         pMoniker->Release();
      if (pStreamConfig)
         pStreamConfig->Release();
   }

   return hr;
}

HRESULT Webcam::AttachToWindow(HWND hwnd)
{
   if (!_initialized || !_windowlessControl)
      return E_FAIL;

   _hwnd = hwnd;

   // Position and size the video
   RECT rcDest;
   rcDest.left = 0;
   rcDest.right = _width;
   rcDest.top = 0;
   rcDest.bottom = _height;
   _windowlessControl->SetVideoClippingWindow(hwnd);

   return _windowlessControl->SetVideoPosition(nullptr, &rcDest);
}

HRESULT Webcam::Start()
{
   if (!_initialized || !_graphBuilder || !_mediaControl)
      return E_FAIL;

   _graphBuilder->Render(_pin);
   return _mediaControl->Run();
}

HRESULT Webcam::Pause()
{
   if (!_initialized || !_mediaControl)
      return E_FAIL;

   return _mediaControl->Pause();
}

HRESULT Webcam::Stop()
{
   if (!_initialized || !_mediaControl)
      return E_FAIL;

   return _mediaControl->Stop();
}

HRESULT Webcam::Repaint()
{
   if (!_initialized || !_windowlessControl)
      return E_FAIL;

   auto hdc = GetDC(_hwnd);
   return _windowlessControl->RepaintVideo(_hwnd, hdc);
}

HRESULT Webcam::Terminate()
{
   auto hr = Stop();

   if (_pin)
   {
      _pin->Disconnect();
      _pin->Release();
   }

   if (_graphBuilder)
      _graphBuilder->Release();
   if (_mediaControl)
      _mediaControl->Release();
   if (_windowlessControl)
      _windowlessControl->Release();
   if (_baseFilter)
      _baseFilter->Release();

   return hr;
}

int Webcam::GetWidth()
{
   return _width;
}

int Webcam::GetHeight()
{
   return _height;
}