local request = require 'common/request'

-- test http request
request.get({ act = 'get_conf' })
:subscribe(function (data) 
  print('success')
  print(data.scrollText)
end, function (error)
  print(error.desc)
end)
