﻿using System;

namespace Gucci.DataAccessLayer.DataTransferObject
{
    public class DefaultEventSearchDto
    {
        public int Uid { get; set; }
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
    }
}
