extends "res://Scenes/Towers/Melee.gd"

func _ready():
	built = true
	health_bar = HealthBar.instantiate()
	add_child(health_bar)
	health_bar.max_value = hp
	health_bar.value = hp
	super._ready()

func hit():
	target.get_hit(attack)
	
func get_next_target() -> Enemy:
	enemies_blocked.sort_custom(Callable(self, "_health_sort"))
	if enemies_blocked.is_empty() : 
		return null
	else :
		return enemies_blocked.get(0)
	
func _health_sort(enemy1 : Enemy, enemy2 : Enemy) -> bool:
	return enemy1.hp < enemy2.hp
