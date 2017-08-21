// JitLogger.h


#pragma once
#include "jitinjection.h"
#pragma managed
using namespace System;
namespace JitDebug {

	public ref class JitLogger
	{
	public:
		static property bool Enabled
		{
			bool get()
			{
				return isHooked;
			}
			void set(bool value)
			{
				if ( value )
				{
					HookJIT();
				}
				else
				{
					UnhookJit();
				}

			}
		};
		static property int JitCompileCount
		{
			int get()
			{
				return jitCount;
			}
		}

	};
}
