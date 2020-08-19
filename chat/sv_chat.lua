RegisterServerEvent('chat:init')
RegisterServerEvent('chat:addTemplate')
RegisterServerEvent('chat:addMessage')
RegisterServerEvent('chat:addSuggestion')
RegisterServerEvent('chat:removeSuggestion')
RegisterServerEvent('_chat:messageEntered')
RegisterServerEvent('chat:clear')
RegisterServerEvent('__cfx_internal:commandFallback')

AddEventHandler('_chat:messageEntered', function(author, color, message)
    if not message or not author then
        return
    end

    TriggerEvent('chatMessage', source, author, message)

    if not WasEventCanceled() then
	local player = source
	if exports.discord_perms:IsRolePresent(player, "Owner") then
	        TriggerClientEvent('chatMessage', -1, "^8^*[Owner] ^7^r".. author .. ":",  { 255, 255, 255 }, message)
	elseif exports.discord_perms:IsRolePresent(player, "Head Admin") then
	        TriggerClientEvent('chatMessage', -1, "^3^*[Head Admin] ^7^r".. author .. ":",  { 255, 255, 255 }, message)
	elseif exports.discord_perms:IsRolePresent(player, "Admin") then
	        TriggerClientEvent('chatMessage', -1, "^3^*[Admin] ^7^r".. author .. ":",  { 255, 255, 255 }, message)	
	elseif exports.discord_perms:IsRolePresent(player, "Developer") then
	        TriggerClientEvent('chatMessage', -1, "^2^*[Developer] ^7^r".. author .. ":",  { 255, 255, 255 }, message)	
	elseif exports.discord_perms:IsRolePresent(player, "Mod Creators") then
	        TriggerClientEvent('chatMessage', -1, "^9^*[Mod Creators] ^7^r".. author .. ":",  { 255, 255, 255 }, message)	
	elseif exports.discord_perms:IsRolePresent(player, "Moderator") then
	        TriggerClientEvent('chatMessage', -1, "^1^*[Moderator] ^7^r".. author .. ":",  { 255, 255, 255 }, message)	
	else
	        TriggerClientEvent('chatMessage', -1, author,  { 255, 255, 255 }, message)			
	end
    end

    print(author .. '^7: ' .. message .. '^7')
end)

AddEventHandler('__cfx_internal:commandFallback', function(command)
    local name = GetPlayerName(source)

    TriggerEvent('chatMessage', source, name, '/' .. command)

    if not WasEventCanceled() then
        TriggerClientEvent('chatMessage', -1, name, { 255, 255, 255 }, '/' .. command) 
    end

    CancelEvent()
end)

-- player join messages
AddEventHandler('chat:init', function()
    TriggerClientEvent('chatMessage', -1, '', { 255, 255, 255 }, '^1^*Members Paradise SERVER: ^r^7' .. GetPlayerName(source) .. ' joined.')
end)

AddEventHandler('playerDropped', function(reason)
    TriggerClientEvent('chatMessage', -1, '', { 255, 255, 255 }, '^1^*Members Paradise SERVER: ^r^7' .. GetPlayerName(source) ..' left (' .. reason .. ')')
end)

RegisterCommand('say', function(source, args, rawCommand)
    TriggerClientEvent('chatMessage', -1, (source == 0) and 'console' or GetPlayerName(source), { 255, 255, 255 }, rawCommand:sub(5))
end)

-- command suggestions for clients
local function refreshCommands(player)
    if GetRegisteredCommands then
        local registeredCommands = GetRegisteredCommands()

        local suggestions = {}

        for _, command in ipairs(registeredCommands) do
            if IsPlayerAceAllowed(player, ('command.%s'):format(command.name)) then
                table.insert(suggestions, {
                    name = '/' .. command.name,
                    help = ''
                })
            end
        end

        TriggerClientEvent('chat:addSuggestions', player, suggestions)
    end
end

AddEventHandler('chat:init', function()
    refreshCommands(source)
end)

AddEventHandler('onServerResourceStart', function(resName)
    Wait(500)

    for _, player in ipairs(GetPlayers()) do
        refreshCommands(player)
    end
end)
