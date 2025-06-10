extends Area2D

var targets_in_range : Array[Node2D] = []

func _on_body_entered(body: Node2D) -> void:
	if body.has_signal("despawn"):
		targets_in_range.append(body)
		body.despawn.connect(func(): _on_body_exited(body))

func _on_body_exited(body: Node2D) -> void:
	var idx = targets_in_range.find(body)
	if idx != -1:
		targets_in_range.remove_at(idx)

func sort_by_priority() -> void:
	return

func find_nearest_body() -> Node2D:
	sort_by_priority()
	if targets_in_range.is_empty() : 
		return null
	else :
		return targets_in_range.get(0)
