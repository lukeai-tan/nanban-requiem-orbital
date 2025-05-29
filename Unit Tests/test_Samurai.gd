extends "res://addons/gut/test.gd"

var SamuraiScene = preload("res://Scenes/Enemies/Samurai.tscn")
var _samurai = null

func before_each():
	_samurai = SamuraiScene.instantiate()
	add_child(_samurai)
	autofree(_samurai)
	_samurai.hp = 100.0

func after_each():
	if is_instance_valid(_samurai):
		_samurai.queue_free()

func test_take_damage():
	_samurai.get_hit(10.0)
	var result = _samurai.hp
	assert_eq(result, 90.0, "HP should be 90.0")

func test_damage_equals_hp():
	_samurai.get_hit(100.0)
	var result = _samurai.hp
	assert_eq(result, 0.0, "HP should be 0.0")

func test_damage_higher_than_hp():
	_samurai.get_hit(120.0)
	var result = _samurai.hp
	assert_eq(result, 0.0, "HP should be 0.0")
