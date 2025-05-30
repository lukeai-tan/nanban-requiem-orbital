extends "res://Scenes/Enemies/Samurai.gd"

func _ready():
	super._ready()
	movement_speed = 200.0
	attack = 200
	
func _process(delta):
	super._process(delta)

func blocked(unit : Unit) :
	super.blocked(unit)
	
