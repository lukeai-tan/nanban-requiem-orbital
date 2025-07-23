extends Node

var username: String = "Player"

func _ready():
    username = OS.get_environment("USERNAME")
    if username.is_empty():
        username = OS.get_environment("USER")
    if username.is_empty():
        username = "Player"