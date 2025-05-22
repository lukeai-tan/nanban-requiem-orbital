extends Node2D

const Enemy = preload("res://Scenes/Enemies/Samurai.gd")
var projectile_scene : PackedScene = load("res://Scenes/Projectile/Projectile.tscn")
var attack : float
var projectile_speed : float
var attack_speed : float
var target : Enemy


func _physics_process(_delta: float) -> void:
	turn()

# rotation of turrets on Ranged towers
func turn():
	var enemy_pos = get_global_mouse_position()
	get_node("Turret").look_at(enemy_pos)

func shoot() :
	var projectile = projectile_scene.instantiate()
	projectile.global_position = global_position
	projectile.damage = attack
	projectile.speed = projectile_speed
	projectile.target = target
	get_tree().current_scene.get_node("Projectile").add_child(projectile)
	
