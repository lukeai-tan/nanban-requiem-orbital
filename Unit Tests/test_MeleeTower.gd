extends "res://addons/gut/test.gd"

var melee_tower = preload("res://Scenes/Towers/MeleeTower0.gd")
var _tower = null

func before_each():
	_tower = melee_tower.new()
	_tower.built = true
	_tower._ready()
	_tower.hp = 100.0
	
func after_each():
	_tower.free()

func test_take_damage():
	_tower.get_hit(10.0)
	var result = _tower.hp
	assert_eq(result, 90.0, "HP should be 90.0")

func test_damage_equals_hp():
	_tower.get_hit(100.0)
	var result = _tower.hp
	assert_eq(result, 0.0, "HP should be 0.0")

func test_damage_higher_than_hp():
	_tower.get_hit(120.0)
	var result = _tower.hp
	assert_eq(result, 0.0, "HP should be 0.0")
