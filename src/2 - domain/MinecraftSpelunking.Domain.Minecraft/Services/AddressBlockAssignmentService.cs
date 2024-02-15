using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinecraftSpelunking.Domain.Common.Services;
using MinecraftSpelunking.Domain.Database;
using MinecraftSpelunking.Domain.Identity.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Entities;
using MinecraftSpelunking.Domain.Minecraft.Common.Enums;
using MinecraftSpelunking.Domain.Minecraft.Common.Services;

namespace MinecraftSpelunking.Domain.Minecraft.Services
{
    internal class AddressBlockAssignmentService : BaseEntityService<AddressBlockAssignment>, IAddressBlockAssignmentService
    {
        private readonly IAddressBlockService _blocks;
        private readonly IJavaServerService _javaServers;

        public AddressBlockAssignmentService(IAddressBlockService blocks, IJavaServerService javaServers, DataContext context, IMapper mapper) : base(x => x.AddressBlockAssignments, context, mapper)
        {
            _blocks = blocks;
            _javaServers = javaServers;
        }

        public async Task<AddressBlockAssignment[]> GetAssignmentsAsync(User user, int count)
        {
            List<AddressBlockAssignment> assignments = new List<AddressBlockAssignment>();

            for (int i = 0; i < count; i++)
            {
                AddressBlock? block = await _blocks.GetAssignableAddressBlockAsync();
                if (block is null)
                {
                    break;
                }

                AddressBlockAssignment? assignment = this.TryAssignAddressBlockAsync(block, user);
                await this.context.SaveChangesAsync();

                if (assignment is null)
                {
                    break;
                }

                assignments.Add(assignment);
            }

            if (assignments.Count == 0)
            {
                return Array.Empty<AddressBlockAssignment>();
            }

            await this.context.SaveChangesAsync();

            return assignments.ToArray();
        }

        public async Task<bool> TryCompleteAssignmentsAsync(int id, User user, Server[] javaServers)
        {
            AddressBlockAssignment? assignment = await this.entities.Include(x => x.Block).FirstOrDefaultAsync(x => x.Id == id);
            if (assignment is null)
            {
                return false;
            }

            foreach (Server server in javaServers)
            {
                await _javaServers.AddAsync(server, assignment.Block);
            }

            assignment.Block.Status = AddressBlockStatusEnum.Scanned;

            return true;
        }

        private AddressBlockAssignment? TryAssignAddressBlockAsync(AddressBlock block, User user)
        {
            block.Status = AddressBlockStatusEnum.Assigned;
            block.ModifiedAt = DateTime.Now;

            AddressBlockAssignment assignment = new AddressBlockAssignment()
            {
                User = user,
                Block = block,
                AssignedAt = DateTime.Now
            };

            this.entities.Add(assignment);

            return assignment;
        }
    }
}
