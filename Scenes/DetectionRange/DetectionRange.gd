extends Area2D

var targets_in_range : Array[Node2D] = []
var curr_target : Node2D

func _on_body_entered(body: Node2D) -> void:
	if body.has_signal("despawn"):
		targets_in_range.append(body)
		body.despawn.connect(func(): _on_body_exited(body))

func _on_body_exited(body: Node2D) -> void:
	if body == curr_target :
		curr_target = null
	else :
		var idx = targets_in_range.find(body)
		if idx != -1:
			targets_in_range.remove_at(idx)

func sort_by_priority() -> void:
	return

func find_nearest_body() -> Node2D:
	sort_by_priority()
	if targets_in_range.is_empty() : 
		curr_target = null
	else :
		curr_target = targets_in_range.pop_front()
	return curr_target
		
func still_in_range() -> bool:
	return curr_target != null
