using Lucene.Net.Support;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Olle.Models;
using Olle.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using static Olle.Models.Helpers.enmus;
using Newtonsoft.Json.Linq;

namespace Olle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OlleController : ControllerBase
    {
        [HttpGet]
        public string olleRequestQuery(string param)
        {
            string response = "";


            Uri uri = new Uri("http://ik.olleco.net/morse-api");

            using (WebClient wc = new WebClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(param))
                        return "Hatalı Giriş Yaptınız Tekrar Deneyiniz";
                    
                    commands r;
                    bool result = Enum.TryParse(param.ToLower(), out r);

                    if (!result)
                    {
                        return "Hatalı Giriş Yaptınız Tekrar Deneyiniz";
                    }
                    else
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        IncoderMorse encodeToMorse = new IncoderMorse();
                        encodeToMorse.InitializeDictionary();
                        string bodyContent = "";
                        bodyContent = "command=" + encodeToMorse.Translate(param.ToString().ToLower());
                        response = response + wc.UploadString(uri, bodyContent);
                        if(param==commands.cpu.ToString())
                        {
                            var res = JsonConvert.DeserializeObject<CPU>(response);
                            res.checksum = new DecoderMorse().FromMorse(res.checksum);
                            foreach(var item in res.data)
                            {   
                                item.model= new DecoderMorse().FromMorse(item.model);
                                item.speed= new DecoderMorse().FromMorse(item.speed);
                                item.times.idle = new DecoderMorse().FromMorse(item.times.idle);
                                item.times.irq = new DecoderMorse().FromMorse(item.times.irq);
                                item.times.nice = new DecoderMorse().FromMorse(item.times.nice);
                                item.times.sys = new DecoderMorse().FromMorse(item.times.sys);
                                item.times.user = new DecoderMorse().FromMorse(item.times.user);
                            }
                            response = JsonConvert.SerializeObject(res);
                        }
                        else
                        {
                            DefaultResult res = JsonConvert.DeserializeObject<DefaultResult>(response);
                            res.checksum = new DecoderMorse().FromMorse(res.checksum);
                            res.data = new DecoderMorse().FromMorse(res.data);
                            response = JsonConvert.SerializeObject(res);
                        }
                       

                    }
                }
                catch (WebException myexp)
                {
                    Console.WriteLine(myexp);
                }
            }

            return response;
        }

        // GET api/<Ello>/5
        [HttpGet("{command}")]
        public string Get(string command)
        {
            var result = olleRequestQuery(command);

            return result;
        }

    }

}
