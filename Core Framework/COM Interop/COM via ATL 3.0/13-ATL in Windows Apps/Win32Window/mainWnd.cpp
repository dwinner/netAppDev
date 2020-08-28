/****************************
 *
 * A raw Windows window.
 *
 ***************************/

 #include <windows.h>

// The callback.
LRESULT CALLBACK MyWindowProc(HWND, UINT, WPARAM, LPARAM);

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrevInst, 
				   PSTR sxCommand, int iShow)
{
	// We need to create a WNDCLASSEX strucure to 
	// hold the details of this window.
	WNDCLASSEX myWindow;
	myWindow.cbClsExtra = 0;
	myWindow.cbSize = sizeof(myWindow);
	myWindow.cbWndExtra = 0;
	myWindow.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
	myWindow.hCursor = LoadCursor(NULL, IDC_ARROW);
	myWindow.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	myWindow.hIconSm = LoadIcon(NULL, IDI_APPLICATION);
	myWindow.hInstance = hInst;
	myWindow.lpfnWndProc = MyWindowProc;
	myWindow.lpszClassName = "MyWin32Window";
	myWindow.lpszMenuName = NULL;
	myWindow.style = CS_HREDRAW | CS_VREDRAW;

	// Register the class.
	RegisterClassEx(&myWindow);

	// Create and show the window.
	HWND hwnd;
	hwnd = CreateWindow("MyWin32Window", 
						"My Raw Window", 
						WS_OVERLAPPEDWINDOW, 
						10, 10, 200, 200, 
						NULL, 
						NULL, 
						hInst, 
						NULL);

	ShowWindow(hwnd, iShow);
	UpdateWindow(hwnd);
	
	// Create the windows message pump.
	MSG msg;
	while(GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return msg.wParam;
}


LRESULT CALLBACK MyWindowProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	HDC hDC;
	PAINTSTRUCT ps;
	RECT clientRect;

	// Which message have we been given?
	switch(msg)
	{
	// We are coming to life.
	case WM_CREATE:
		MessageBox(NULL, "Your window is about to be created",
				   "Message from WM_CREATE", MB_OK | MB_SETFOREGROUND);
		return 0;

	// We need to be rendered.
	case WM_PAINT:

		hDC = BeginPaint(hwnd, &ps);
		GetClientRect(hwnd, &clientRect);

		DrawText(hDC, "The Raw Win32 Window", -1,
			     &clientRect, DT_CENTER | DT_VCENTER | DT_SINGLELINE);
		EndPaint(hwnd, &ps);

		return 0;

	// We must die!
	case WM_DESTROY:
		PostQuitMessage(0); 
		return 0;
	}

	// Let system handle everything else.
	return DefWindowProc(hwnd, msg, wParam, lParam);
}