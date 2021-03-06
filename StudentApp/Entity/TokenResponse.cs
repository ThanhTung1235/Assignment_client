﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entity
{
    class TokenResponse
    {
        private string _accessToken;
        private int _ownerId;
        private int _clazzId;
        public string accessToken { get => _accessToken; set => _accessToken = value; }
        public int OwnerId { get => _ownerId; set => _ownerId = value; }
        public int ClazzId { get => _clazzId; set => _clazzId = value; }
    }
}
