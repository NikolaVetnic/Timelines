using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Reminders.Dtos;
using BuildingBlocks.Domain.Reminders.ValueObjects;
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
}
