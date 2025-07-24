extends HBoxContainer

var tower_builder

# Takes in reference to TowerBuilder instance and loads tower buttons based on Towers set to true in GameData
# Each button is connected to initiate_build_mode in TowerBuilder

func setup(tower_builder_ref):
    tower_builder = tower_builder_ref
    _create_buttons()

func _create_buttons():
    for key in GameData["tower_data"]:

        var entry = GameData["tower_data"][key]
        if not GameData["tower_squad"][key] or !GameData["tower_squad"][key]:
            continue

        var tower_button = TextureButton.new()
        tower_button.name = key
        tower_button.texture_normal = load("res://Assets/UI/Buttons/button_square_depth_gradient.png")
        tower_button.texture_pressed = load("res://Assets/UI/Buttons/button_square_flat.png")
        tower_button.texture_hover = load("res://Assets/UI/Buttons/button_square_depth_flat.png")
        tower_button.custom_minimum_size = Vector2(85, 85)
        tower_button.ignore_texture_size = true
        tower_button.stretch_mode = TextureButton.STRETCH_KEEP_ASPECT

        var icon = TextureRect.new()
        icon.texture = load(entry["sprite_icon"])
        icon.expand_mode = TextureRect.EXPAND_FIT_WIDTH
        icon.stretch_mode = TextureRect.STRETCH_KEEP_ASPECT_CENTERED
        icon.size = Vector2(85, 85)
        icon.scale =  Vector2(0.80, 0.80)
        icon.position = Vector2(10, 10)
        
        icon.anchor_left = 0
        icon.anchor_top = 0
        icon.anchor_right = 1
        icon.anchor_bottom = 1
        icon.offset_left = 0
        icon.offset_top = 0
        icon.offset_right = 20
        icon.offset_bottom = 12
        icon.texture_filter = CanvasItem.TEXTURE_FILTER_NEAREST        
        icon.mouse_filter = Control.MOUSE_FILTER_IGNORE

        tower_button.add_child(icon)

        tower_button.connect("pressed", Callable(tower_builder, "initiate_build_mode").bind(tower_button.name))
        add_child(tower_button)
