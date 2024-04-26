#region

using Chipsoft.Assignments.EPDDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Chipsoft.Assignments.EPDCInfrastructure.Configuration;

public class PhysicianConfiguration: IEntityTypeConfiguration<Physician>
{
    public void Configure(EntityTypeBuilder<Physician> builder)
    {
    }
}