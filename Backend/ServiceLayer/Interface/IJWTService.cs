﻿using System.Collections.Generic;

namespace ServiceLayer.Interface
{
    public interface IJWTService
    {
        string CreateToken(string username, int userId);
        bool CheckUserClaims(string jwtToken, List<string> claimsToCheck);
        int GetUserIDFromToken(string jwtToken);
        bool IsJWTSignatureTampered(string userJwtToken);
    }
}
