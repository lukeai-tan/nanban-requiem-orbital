extends Node

func _ready():
	load_main_menu()

func load_main_menu():
	if not has_node("MainMenu"):
		var main_menu = load("res://Scenes/UIScenes/MainMenu.tscn").instantiate()
		add_child(main_menu)

	var new_game_button = get_node("MainMenu/Margin/VB_buttons/NewGame")
	if not new_game_button.is_connected("pressed", Callable(self, "on_new_game_pressed")):
		new_game_button.connect("pressed", Callable(self, "on_new_game_pressed"))

	var journal_button = get_node("MainMenu/Margin/VB_buttons/Journal")
	if not journal_button.is_connected("pressed", Callable(self, "on_journal_pressed")):
		journal_button.connect("pressed", Callable(self, "on_journal_pressed"))

	var settings_button = get_node("MainMenu/Margin/VB_buttons/Settings")
	if not settings_button.is_connected("pressed", Callable(self, "on_settings_pressed")):
		settings_button.connect("pressed", Callable(self, "on_settings_pressed"))

	var quit_button = get_node("MainMenu/Margin/VB_buttons/Quit")
	if not quit_button.is_connected("pressed", Callable(self, "on_quit_pressed")):
		quit_button.connect("pressed", Callable(self, "on_quit_pressed"))
	


func on_new_game_pressed():
	$"MainMenu".queue_free()
	print("New Game button pressed")

	var map_selector = load("res://Scenes/UIScenes/MapSelector.tscn").instantiate()
	map_selector.connect("map_selected", Callable(self, "_on_map_selected"))
	map_selector.connect("return_to_main_menu", Callable(self, "_on_ui_returned").bind(map_selector))
	call_deferred('add_child', map_selector)
	

func on_journal_pressed():
	print("Journal button pressed")
	print("Yes Ziming, the fonts for all the buttons are same now")
	var journal_scene: Control = load("res://Scenes/UIScenes/Journal.tscn").instantiate()
	journal_scene.connect("return_to_main_menu", Callable(self, "_on_ui_returned").bind(journal_scene))
	call_deferred('add_child', journal_scene)

func on_settings_pressed():
	print("Settings button pressed")
	var volume_settings_scene: Control = load("res://Scenes/UIScenes/Settings.tscn").instantiate()
	volume_settings_scene.connect("return_to_main_menu", Callable(self, "_on_ui_returned").bind(volume_settings_scene))
	call_deferred('add_child', volume_settings_scene)

func on_quit_pressed():
	get_tree().quit()

func _on_ui_returned(ui_node):
	ui_node.queue_free()
	load_main_menu()

func _on_map_selected(map_name: String):
	var game_scene: Node2D
	GameData["tower_squad"] = GameData[map_name + "_tower_squad"]
	if(GameData.chicken_don_dead):
		GameData["tower_squad"]["RangedTowerChicken"] = true
	GameData["tower_squad"]["RangedTowerChicken"] = true

	if map_name == "BossMap":
		game_scene = load("res://Scenes/MainScenes/BossStageManager.tscn").instantiate()
		#game_scene.connect("GameFinished", unload_game)
		add_child(game_scene)
		game_scene.Initialize()
		var end_game_screen = game_scene.get_node("UI/EndGameScreen")
		end_game_screen.connect("game_finished", unload_game)

	elif map_name == "Map5":
		game_scene = load("res://Scenes/MainScenes/ChickenStageManager.tscn").instantiate()
		game_scene.set("map_to_load", map_name)
		#game_scene.connect("game_finished", unload_game)
		add_child(game_scene)
		var end_game_screen = game_scene.get_node("UI/EndGameScreen")
		end_game_screen.connect("game_finished", unload_game)

	else:
		game_scene = load("res://Scenes/MainScenes/GameScene.tscn").instantiate()
		game_scene.set("map_to_load", map_name)
		#game_scene.connect("game_finished", unload_game)
		add_child(game_scene)
		var end_game_screen = game_scene.get_node("UI/EndGameScreen")
		end_game_screen.connect("game_finished", unload_game)

func unload_game(_result):
	var game_scene = get_node_or_null("GameScene")
	if game_scene:
		game_scene.queue_free()
	
	var existing_menu = get_node_or_null("MainMenu")
	if existing_menu:
		existing_menu.queue_free()
	
	call_deferred("_deferred_load_main_menu")

func _deferred_load_main_menu():
	var main_menu = load("res://Scenes/UIScenes/MainMenu.tscn").instantiate()
	add_child(main_menu)
	load_main_menu()
	
