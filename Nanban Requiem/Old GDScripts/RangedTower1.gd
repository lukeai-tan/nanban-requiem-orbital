extends "res://Scenes/Towers/Ranged.gd"

var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")

func shoot() :
	var projectile = projectile_scene.instantiate()
	projectile.initialize(attack, projectile_speed, target, global_position)
	projectiles_node.add_child(projectile)
