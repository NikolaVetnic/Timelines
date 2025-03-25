using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Reminders.Application.Data.Abstractions;

namespace Reminders.Application.Data;

public class RemindersService(IServiceProvider serviceProvider, IRemindersRepository remindersRepository) : IRemindersService
{
    public async Task<ReminderDto> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken)
    {
        var reminder = await remindersRepository.GetReminderByIdAsync(reminderId, cancellationToken);
        var reminderDto = reminder.Adapt<ReminderDto>();

        var node = await serviceProvider.GetRequiredService<INodesService>().GetNodeBaseByIdAsync(reminder.NodeId, cancellationToken);
        reminderDto.Node = node;
        
        return reminderDto;
    }

    public async Task<ReminderBaseDto> GetReminderBaseByIdAsync(ReminderId reminderId, CancellationToken cancellationToken)
    {
        var reminder = await remindersRepository.GetReminderByIdAsync(reminderId, cancellationToken);
        var reminderBaseDto = reminder.Adapt<ReminderBaseDto>();
        
        return reminderBaseDto;
    }
    
    public async Task<List<ReminderBaseDto>> GetRemindersBaseBelongingToNodeIdsAsync(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        var reminders = await remindersRepository.GetRemindersBelongingToNodeIdsAsync(nodeIds, cancellationToken);
        var reminderBaseDtos = reminders.Adapt<List<ReminderBaseDto>>();
        return reminderBaseDtos;
    }

}
