﻿using System;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace LogService.Controllers
{
    [ApiController]
    [Route("log")]
    public class LogController : ControllerBase
    {
        private readonly ITracer _tracer;

        public LogController(ITracer tracer)
        {
            _tracer = tracer;
        }

        [HttpGet("compute")]
        public ActionResult<double> Compute(int n, int x)
        {
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log($"Requested log compute of #{n}, base {x}");
            return Ok(Math.Log(n) / Math.Log(x));
        }
    }
}