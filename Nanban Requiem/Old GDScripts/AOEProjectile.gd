extends "res://Scenes/Projectile/Projectile.gd"

const AOE = preload("res://Scenes/Projectile/AOE.gd")
const AOEScene = preload("res://Scenes/Projectile/AOE.tscn")

var AOEDamage : float = 25

func hit_target() :
	var AreaEffect = AOEScene.instantiate()
	AreaEffect.initialize(AOEDamage, global_position)
	get_parent().add_child(AreaEffect)
	target.get_hit(damage)
	queue_free()
