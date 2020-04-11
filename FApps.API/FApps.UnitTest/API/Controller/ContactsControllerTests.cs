using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FApps.API.Controllers;
using FApps.Core.Domain;
using FApps.Core.DTO;
using FApps.Services.Contact.Command;
using FApps.Services.Contact.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FApps.UnitTest.API.Controller
{
    [TestFixture]
    public class ContactsControllerTests
    {
        private Mock<IMediator> _mediator;
        private ContacsController _target;
        private IFixture _fixture;
        
        [SetUp]
        public void setUp()
        {
            _mediator = new Mock<IMediator>();
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _target = new ContacsController(_mediator.Object);
        }

        [TearDown]
        public void tearDown()
        {
            _mediator = null;
            _target = null;
            _fixture = null;
        }

        #region Get All Records

        [Test]
        public async Task  whenQueryingContacts_ControllerReturnsValidResponse()
        {
            var expected = _fixture.Create<IList<ContactDTO>>();
            _mediator.Setup(x => x.Send(It.IsAny<ReadQuery>(), default(CancellationToken))).ReturnsAsync(expected);

            var result = await _target.GetAll() as ObjectResult;
            Assert.IsInstanceOf<ObjectResult>(result);

        }
        #endregion

        #region Get By Id
        [Test]
        public async Task whenQueryingContacts_InputIsEmptyString_ControllerReturnBadRequest()
        {
            var result = await _target.GetById(Guid.Empty);
            Assert.That(result, Is.InstanceOf<NotFoundResult>());

        }
        [Test]
        public async Task whenQueryingContacts_ValidInput_ControllerReturnValidResponse()
        {
            var input = "c09e1c5b-fa74-4436-aded-4fcb7bc9c7d2";
            var expected = _fixture.Create<Task<Contact>>();

            _mediator.Setup(x => x.Send(It.IsAny<ReadByIdQuery>(), default(CancellationToken))).Returns(expected);

            var result = await _target.GetById(new Guid(input)) as ObjectResult;
            Assert.IsInstanceOf<ObjectResult>(result);
        }

        #endregion

        #region Create Contacts
        [Test]
        public async Task whenCreatingContacts_InvalidModelState_ControllerReturnedBadRequest()
        {
            const Contact input = null;
            var expected = _fixture.Create<Guid>();

            _mediator.Setup(x => x.Send(It.IsAny<InsertCommand>(), default(CancellationToken))).ReturnsAsync(expected);

            var result = await _target.Create(input) as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task whenCreatingContacts_ValidInput_ControllerReturnsValidResponse()
        {
            Contact input = new Contact();
            var expected = _fixture.Create<Guid>();

            _mediator.Setup(x => x.Send(It.IsAny<InsertCommand>(), default(CancellationToken))).ReturnsAsync(expected);

            var result = await _target.Create(input) as ObjectResult;
            Assert.IsInstanceOf<CreatedResult>(result);
            Assert.NotNull(result);
        }
        #endregion

        #region Update Contacts
        [Test]
        public async Task whenUpdatingContacts_ModelisNull_ControllerReturnsBadRequest()
        {
            var input = "c09e1c5b-fa74-4436-aded-4fcb7bc9c7d2";

            _mediator.Setup(x => x.Send(It.IsAny<UpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<bool>());
            var result = await _target.Update(new Guid(input), It.IsAny<Contact>());

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.NotNull(result);
        }

        [Test]
        public async Task whenUpdatingContacts_validInput_ControllerReturnsValidResponse()
        {
            var input = "c09e1c5b-fa74-4436-aded-4fcb7bc9c7d2";
            var model = _fixture.Create<Contact>();

            _mediator.Setup(x => x.Send(It.IsAny<UpdateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(It.IsAny<bool>);

            var result = await _target.Update(new Guid(input), model);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.NotNull(result);
        }
        #endregion

        #region Delete
        [Test]
        public async Task whenDeletingContacts_InputIsNull_ControllerReturnsBadRequestObjectResult()
        {
            var result = await _target.Delete(null) as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        [Test]
        public async Task whenDeletingContact_InputIsEmptyString_ControllerReturnsBadRequestObjectResult()
        {
            var result = await _target.Delete(Guid.Empty) as ObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

    }
}
