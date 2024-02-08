using AutoMapper;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Models;
using LtAmpDotNet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtAmpDotNet.Design
{
    internal class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel() : base((AmpStateModel)App.Current.Services.GetService(typeof(AmpStateModel)), (IMapper)App.Current.Services.GetService(typeof(IMapper)))
        {

        }
    }
}
