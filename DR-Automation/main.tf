# DR site for the BP calculator in Spain Central (prod runs in Austria East).
# Runs from GitHub Actions (dr-infra-create.yml); Azure login comes from OIDC via the ARM_* env vars.

terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0"
    }
  }

  # State lives in Azure so every pipeline run remembers what it built.
  # The storage account name is passed in by the workflow.
  backend "azurerm" {
    resource_group_name = "TUD-CD-DR"
    container_name      = "tfstate"
    key                 = "dr.tfstate"
    use_oidc            = true
    use_azuread_auth    = true
  }
}

# DR region. Azure for Students only allows: austriaeast, switzerlandnorth,
# denmarkeast, spaincentral, germanywestcentral. Prod is austriaeast.
locals {
  location = "spaincentral"
}

provider "azurerm" {
  features {}
}

# The resource group is created by hand in the portal,
# so the pipeline only needs Contributor on this group.
data "azurerm_resource_group" "dr" {
  name = "TUD-CD-DR"
}

# App Service plan: Linux, free F1 tier
resource "azurerm_service_plan" "dr" {
  name                = "BP-Calc-BR"
  resource_group_name = data.azurerm_resource_group.dr.name
  location            = local.location
  os_type             = "Linux"
  sku_name            = "F1"
}

# Web app: same setup as prod, running the same Docker Hub image
resource "azurerm_linux_web_app" "dr" {
  name                = "BP-Calc-app-DR"
  resource_group_name = data.azurerm_resource_group.dr.name
  location            = local.location
  service_plan_id     = azurerm_service_plan.dr.id
  https_only          = true

  site_config {
    always_on           = false # not available on F1
    minimum_tls_version = "1.2"
    ftps_state          = "Disabled"

    application_stack {
      docker_image_name   = "praveenps025/bpcalc:latest"
      docker_registry_url = "https://index.docker.io"
    }
  }

  app_settings = {
    WEBSITES_PORT = "5000" # the container listens on 5000
  }
}

output "webapp_url" {
  value = "https://${azurerm_linux_web_app.dr.default_hostname}"
}
