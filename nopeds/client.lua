DensityMultiplier = 0.00
Citizen.CreateThread(function()
	while true do
	    Citizen.Wait(0)
		SetVehicleDensityMultiplierThisFrame(DensityMultiplier)
		SetPedDensityMultiplierThisFrame(DensityMultiplier)
		SetRandomVehicleDensityMultiplierThisFrame(DensityMultiplier)
		SetParkedVehicleDensityMultiplierThisFrame(DensityMultiplier)
		SetScenarioPedDensityMultiplierThisFrame(DensityMultiplier, DensityMultiplier)
		local playerPed = GetPlayerPed(-1)
		local pos = GetEntityCoords(playerPed) 
		RemoveVehiclesFromGeneratorsInArea(pos['x'] - 500.0, pos['y'] - 500.0, pos['z'] - 500.0, pos['x'] + 500.0, pos['y'] + 500.0, pos['z'] + 500.0);
	end
end)