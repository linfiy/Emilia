
--*** config ***--
local HOST = 'http://cangzhou.linfiy.com/mahjong/game_s_http_cangzhou/index.php';
local version = '3.0.4';
-- 没写 ios
local platform = "gfplay"

--*** code ***--
local JSON = require 'lib/json'
local Rx = require 'lib/rx'

request = {}

function request.get (params)
  return Rx.Observable.create(function (observer)
    local url = HOST .. generateParamString(params)
    print('[HTTP] REQ => ' .. decodeURI(url))
    
    CS.Util.Request.Get(
    url, 
    function (res)
      print('[HTTP] RES => ' .. res)
      
      resTable = JSON.decode(res)
      code = resTable.code
      subCode = resTable.sub_code

      if code == 0 and subCode == 0 then
        observer:onNext(resTable.data)
      else
        observer:onError(resTable)
      end
        observer:onCompleted()
    end, 
    function (errorMessage)
      observer:onError({ code = -1, desc = "网络错误" })
      observer:onCompleted()
    end
  )
  end)
  
end

local rootParams = { randkey = '', c_version = version, parameter = {} }
local actExParams = { mod = 'Business', platform = platform }

function generateParamString (actParams)
  
  -- 添加默认项
  for key, value in pairs(actExParams) do 
    actParams[key] = value
  end
  
  rootParams.parameter = encodeURI(JSON.encode(actParams))

  local str = '?'

  for key, value in pairs(rootParams) do
    str = str .. key .. '=' .. value .. '&'
  end

  -- 去掉最后的 '&'
  return string.sub(str, 1, -2)
  
end

function encodeURI(s)
  s = string.gsub(s, "([^%w%.%- ])", function(c) return string.format("%%%02X", string.byte(c)) end)
  return string.gsub(s, " ", "+")
end

function decodeURI(s)
  s = string.gsub(s, '%%(%x%x)', function(h) return string.char(tonumber(h, 16)) end)
  return s
end

return request