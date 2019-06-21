using Cootrasana.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
