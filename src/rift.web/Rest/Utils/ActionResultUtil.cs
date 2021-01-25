﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace rift.web.Rest.Utils
{
    public static class ActionResultUtil
    {
        public static ActionResult WrapOrNotFound(object value)
        {
            return value != null ? (ActionResult)new OkObjectResult(value) : new NotFoundResult();
        }
    }
}
