local this = {};
--[[
this.gameObject;
this.name;
--]]
function this.SetData(arg1)
	log(this.name.."  do func SetData:" .. tostring(arg1));
end

function this.Awake()
	-- body
	log(this.name.."  do func Awake");
end

function  this.Start()
	-- body
	log(this.name.."  do func Start");
end

function  this.OnEnable()
	-- body
	log(this.name.."  do func OnEnable");
end

function  this.OnClick(arg)--arg有可能为nil
	-- body
	log(this.name.."  do func OnClick");
end

function  this.Update()
	-- body
	log(this.name.."  do func Update");
end

function  this.OnDisable()
	-- body
	log(this.name.."  do func OnDisable");
end

function  this.OnDestroy()
	-- body
	log(this.name.."  do func OnDestroy");
end

return this;
