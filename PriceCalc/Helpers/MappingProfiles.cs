using AutoMapper;
using PriceCalc.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCalc.Helpers
{
    /// <summary>
    /// For simplicity I tried to work with AutoMapper, However It`s not working due to not injecting the service in the startup
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

        }
    }
}
