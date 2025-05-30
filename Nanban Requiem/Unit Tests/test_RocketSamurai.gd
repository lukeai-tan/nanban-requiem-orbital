extends "res://addons/gut/test.gd"

var RocketSamuraiScene = preload("res://Scenes/Enemies/RocketSamurai.tscn")
var _rocket_samurai = null

func before_each():
	_rocket_samurai = RocketSamuraiScene.instantiate()
	add_child(_rocket_samurai)
	autofree(_rocket_samurai)
	_rocket_samurai.hp = 100.0

func after_each():
	if is_instance_valid(_rocket_samurai):
		_rocket_samurai.queue_free()

func test_take_damage():
	_rocket_samurai.get_hit(10.0)
	var result = _rocket_samurai.hp
	assert_eq(result, 90.0, "HP should be 90.0")

func test_damage_equals_hp():
	_rocket_samurai.get_hit(100.0)
	var result = _rocket_samurai.hp
	assert_eq(result, 0.0, "HP should be 0.0")

func test_damage_higher_than_hp():
	_rocket_samurai.get_hit(120.0)
	var result = _rocket_samurai.hp
	assert_eq(result, 0.0, "HP should be 0.0")
