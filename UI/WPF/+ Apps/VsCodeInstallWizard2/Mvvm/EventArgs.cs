﻿using System;

namespace MultiStudio.VsCodeInstallWizard.Mvvm
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T value) => Value = value;

        public T Value { get; }
    }
}