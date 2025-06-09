using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Reminders.Reminder.Dtos;
using BuildingBlocks.Domain.Reminders.Reminder.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Reminders.Application.Data.Abstractions;
using Reminders.Application.Entities.Reminders.Extensions;

namespace Reminders.Application.Data;

public class RemindersService(IServiceProvider serviceProvider, IRemindersRepository remindersRepository) : IRemindersService
{
    private INodesService NodesService => serviceProvider.GetRequiredService<INodesService>();

    public async Task<List<ReminderDto>> ListRemindersPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await NodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var reminders = await remindersRepository.ListRemindersPaginatedAsync(r => !r.IsDeleted, pageIndex, pageSize, cancellationToken);

        var reminderDtos = reminders.Select(n =>
            n.ToReminderDto(
                nodes.First(d => d.Id == n.NodeId.ToString())
                )).ToList();

        return reminderDtos;
    }

    public async Task<List<ReminderDto>> ListFlaggedForDeletionRemindersPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var nodes = await NodesService.ListNodesPaginated(pageIndex, pageSize, cancellationToken);

        var reminders = await remindersRepository.ListRemindersPaginatedAsync(r => r.IsDeleted, pageIndex, pageSize, cancellationToken);

        var reminderDtos = reminders.Select(n =>
            n.ToReminderDto(
                nodes.First(d => d.Id == n.NodeId.ToString())
            )).ToList();

        return reminderDtos;
    }

    public async Task<List<ReminderBaseDto>> ListRemindersByNodeIdPaginated(NodeId nodeId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var reminders = await remindersRepository.ListRemindersPaginatedAsync(r => r.NodeId == nodeId, pageIndex, pageSize, cancellationToken);

        var remindersDtos = reminders
            .Select(r => r.ToReminderBaseDto())
            .ToList();

        return remindersDtos;
    }

    public async Task<ReminderDto> GetReminderByIdAsync(ReminderId reminderId, CancellationToken cancellationToken)
    {
        var reminder = await remindersRepository.GetReminderByIdAsync(reminderId, cancellationToken);
        var reminderDto = reminder.Adapt<ReminderDto>();

        var node = await NodesService.GetNodeBaseByIdAsync(reminder.NodeId, cancellationToken);
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

    public async Task<long> CountAllRemindersAsync(CancellationToken cancellationToken)
    {
        return await remindersRepository.CountRemindersAsync(r => true, cancellationToken);
    }

    public async Task<long> CountAllRemindersByNodeIdAsync(NodeId nodeId, CancellationToken cancellationToken)
    {
        return await remindersRepository.CountRemindersAsync(r => r.NodeId == nodeId, cancellationToken);
    }

    public async Task DeleteReminder(ReminderId reminderId, CancellationToken cancellationToken)
    {
        var reminder = await remindersRepository.GetReminderByIdAsync(reminderId, cancellationToken);
        await NodesService.RemoveReminder(reminder.NodeId, reminderId, cancellationToken);

        await remindersRepository.DeleteReminder(reminderId, cancellationToken);
    }

    public async Task DeleteReminders(NodeId nodeId, IEnumerable<ReminderId> reminderIds, CancellationToken cancellationToken)
    {
        var input = reminderIds.ToList();

        await remindersRepository.DeleteReminders(input, cancellationToken);
    }

    public async Task DeleteRemindersByNodeIds(IEnumerable<NodeId> nodeIds, CancellationToken cancellationToken)
    {
        await remindersRepository.DeleteRemindersByNodeIds(nodeIds, cancellationToken);
    }
}
