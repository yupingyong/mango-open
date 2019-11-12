﻿using System;
using System.Collections.Generic;
using System.Text;
using Mango.Web.Models;
namespace Mango.Web.ViewModels
{
    public class NavigationViewModel
    {
        /// <summary>
        /// 网址导航分类数据
        /// </summary>
        public List<NavigationClassifyModel> ClassifyListData { get; set; }
        /// <summary>
        /// 网址导航数据
        /// </summary>
        public List<NavigationModel> NavigationsData { get; set; }
    }
}
