using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsosAppMaui.Dto
{
    public class TokenDto
    {
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
        public bool oauth_callback_confirmed { get; set; }

       

        public static TokenDto from(string text)
        {
            TokenDto tokenDto = new TokenDto();
            string[] split = text.Split("&");
            foreach (string s in split)
            {

                string[] temp = s.Split("=");
                if (temp[0].Equals("oauth_token"))
                    tokenDto.oauth_token = temp[1];
                else if (temp[0].Equals("oauth_token_secret"))
                    tokenDto.oauth_token_secret = temp[1];
                else
                    tokenDto.oauth_callback_confirmed = Boolean.Parse(temp[1]);
            }
            return tokenDto;
        }
    }
}
