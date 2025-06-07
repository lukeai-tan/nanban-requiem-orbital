extends Control

@onready var info_panel = $"Margin/Info Panel"
@onready var sprite = $"Margin/Info Panel/Sprite"
@onready var name_label = $"Margin/Info Panel/Name"
@onready var description_label = $"Margin/Info Panel/Description"
@onready var stats_label = $"Margin/Info Panel/Stats"
@onready var entry_list = $"Margin/ScrollContainer/Entry List"

func _ready():
	info_panel.visible = false
	
func _process(delta: float) -> void:
	pass

'''
func _populate_icons():
	entry_list.get_children().map(func(child): child.queue_free())

	for entry in GameData:
		var icon_button = TextureButton.new()
		icon_button.texture_normal = item["sprite"]
		icon_button.tooltip_text = item["name"]
		icon_button.pressed.connect(_on_icon_pressed.bind(item))
		icon_grid.add_child(icon_button)
'''

func _on_icon_pressed(entry):
	sprite.texture = entry["sprite"]
	name_label.text = entry["name"]
	description_label.text = entry["description"]
	stats_label.text = entry["stats"]
	info_panel.visible = true
