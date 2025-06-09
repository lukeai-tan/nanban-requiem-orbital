extends Node



'''
# Template for Tower Data
"Tower name": {
		"range": Tower range,
		"sprite_icon": "res://filepath",
		"sprite_in_game": "res://filepath",
		"name": "Tower name",
		"description": "Tower description",
		"stats": "Tower stats"
	},
'''






var tower_data = {
	"RangedTower1": {
		"range": 300,
		"sprite_icon": "res://Assets/Towers/RangedTower1_turret.png",
		"sprite_in_game": "res://Assets/Towers/RangedTower1_turret.png",
		"name": "Pew Pew",
		"description": "This is a Pew Pew description",
		"stats": ""
	},
		
	"RangedTower2": {
		"range": 600,
		"sprite_icon": "res://Assets/Towers/RangedTower2_turret.png",
		"sprite_in_game": "res://Assets/Towers/RangedTower2_turret.png",
		"name": "Rocketeer",
		"description": "This is a rocketeer description",
		"stats": "Rocketeer stats"
	},
	
	"Obstacle1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/Obstacle1_base.png",
		"sprite_in_game": "res://Assets/Towers/Obstacle1_base.png",
		"name": "Boulder",
		"description": "This is a boulder description",
		"stats": "Boulder stats"
	},
		
	"MeleeTower1": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/MeleeTower1_base.png",
		"sprite_in_game": "res://Assets/Towers/MeleeTower1_base.png",
		"name": "Inverse Boulder",
		"description": "",
		"stats": ""
	},
	
	"RangedTowerGirl": {
		"range": 600,
		"sprite_icon": "res://Assets/Towers/RangedTowerGirl_icon.png",
		"sprite_in_game": "res://Assets/Towers/RangedTowerGirl_base.png",
		"name": "Insert Girl Name",
		"description": "This is a girl description",
		"stats": "Girl stats"
	},

	"Tokisaki": {
		"range": 0,
		"sprite_icon": "res://Assets/Towers/RangedTowerGirl_icon.png",
		"sprite_in_game": "res://Assets/Towers/RangedTowerGirl_base.png",
		"name": "Tokisaki",
		"description": "Tokisaki blow up",
		"stats": "Tokisaki do nothing"
	},

}
	
var wave_data = {
	"Map1": [
		# Wave 1
		["Samurai", 0.0],
		
		# Wave 2
		["Samurai", 5.0],
		["RocketSamurai", 2.0],
		
		# Wave 3
		["WhiteSamurai", 10.0],
		
		# Wave 4
		["Samurai", 2.0],
		["Samurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		
		# Wave 5
		["WhiteSamurai", 6.0],
		["WhiteSamurai", 2.0],
		
		# Wave 6
		["Samurai", 5.0],
		["Samurai", 1.0],
		["Samurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["RocketSamurai", 1.0],
		["WhiteSamurai", 2.0],
		["WhiteSamurai", 2.0],
		["WhiteSamurai", 1.0],
	],
	
	"Map2": [
		
	],
}
