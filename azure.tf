terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.0.0"
    }
  }
}

provider "azurerm"{
    features {  
    }
}

resource "azurerm_resource_group" "game_tf" {
  name = "gametfrg"
  location = "Canada East"
}

resource "azurerm_container_group" "game_cg_tf" {
  name = "gameapi"
  location = azurerm_resource_group.game_tf.location
  resource_group_name = azurerm_resource_group.game_tf.name

  ip_address_type     = "Public"
  dns_name_label      = "nikunj3011"
  os_type             = "Linux"

  container {
      name            = "games"
      image           = "nikunj3011/games"
        cpu             = "1"
        memory          = "1"

        ports {
            port        = 5000
            protocol    = "TCP"
        }
  }
} 