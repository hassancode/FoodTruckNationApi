﻿using FoodTruckNation.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.Core.AppInterfaces
{
    public interface ISocialMediaPlatformService
    {


        List<SocialMediaPlatform> GetAllSocialMediaPlatforms();

    }
}
