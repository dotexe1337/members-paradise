-- Made by FAXES, forked by dotexe, Based off HRC
Citizen.CreateThread(function()
    local display = true
    local startTime = GetGameTimer()
    local delay = 10000 -- ms

    TriggerEvent('logo:display', true)

    while display do
      Citizen.Wait(0)
	  if(IsControlPressed(1, 38)) then
		display = false
        TriggerEvent('logo:display', false)
	  end
	end
 end)

RegisterNetEvent('logo:display')
AddEventHandler('logo:display', function(value)
  SendNUIMessage({
    type = "logo",
    display = value
  })
end)

function ShowInfo(text, state)
  SetTextComponentFormat("STRING")
  AddTextComponentString(text)
  DisplayHelpTextFromStringLabel(0, state, 0, -1)
end