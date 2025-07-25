extends Control

@export var max_dp: int = 99
@export var current_dp: int = 10
@export var dp_gain_rate: float = 1.0
@onready var dp_label: Label = get_node("DPCount")



signal dp_changed(updated_dp)

var _timer := 0.0

func _ready() -> void:
	dp_label.text = "%d / %d" % [current_dp, max_dp]

func _process(delta: float) -> void:
	_timer += delta
	if _timer >= dp_gain_rate:
		_timer -= dp_gain_rate
		gain_dp(1)
		dp_label.text = "%d / %d" % [current_dp, max_dp]


func gain_dp(amount: int) -> void:
	current_dp = clamp(current_dp + amount, 0, max_dp)
	emit_signal("dp_changed", current_dp)


func can_spend_dp(amount: int) -> bool:
	if current_dp >= amount:
		current_dp -= amount
		emit_signal("dp_changed", current_dp)
		return true
	return false
