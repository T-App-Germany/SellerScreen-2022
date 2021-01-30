using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalShared
{
    public interface IView
    {
    }

    public interface IPluginSecondScreen : IView
    {
    }

    public interface ICalculatorScreen : IView
    {
    }
}
