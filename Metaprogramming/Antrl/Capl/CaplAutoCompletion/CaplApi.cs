using System.Collections.Generic;

namespace CaplAutoCompletion
{
    public class CaplApi
    {
        public CaplApi(ISet<FunctionType> functions, ISet<CaplClass> classes)
        {
            Functions = functions;
            Classes = classes;
        }

        public ISet<FunctionType> Functions { get; }

        public ISet<CaplClass> Classes { get; }
    }
}