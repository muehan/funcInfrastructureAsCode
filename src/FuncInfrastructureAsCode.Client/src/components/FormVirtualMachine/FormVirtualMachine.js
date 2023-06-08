import React from "react";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./FormVirtualMachine.css";

const FormVirtualMachine = () => {

  const {
    virtualMachine,
    setVirtualMachineName,
    setVirtualMachineAdminUsername,
    setVirtualMachineSize,
    setVirtualMachineAdminSshKeyUsername,
    setVirtualMachineAdminSshKeyPublicKey,
    setVirtualMachineOsDiskCachine,
    setVirtualMachineOsDiskStorageAccountType,
    setVirtualMachineSourceImageReferenceOffer,
    setVirtualMachineSourceImageReferencePublisher,
    setVirtualMachineSourceImageReferenceSku,
    setVirtualMachineSourceImageReferenceVersion,
  } = useVirtualMachine();

  const handleVmNameChange = (event) => {
    setVirtualMachineName(event.target.value);
  };

  const handleVmAdminUsernameChange = (event) => {
    setVirtualMachineAdminUsername(event.target.value);
  };

  const handleVmSizeChange = (event) => {
    setVirtualMachineSize(event.target.value);
  };

  const handleVmAdminSshKeyUsernameChange = (event) => {
    setVirtualMachineAdminSshKeyUsername(event.target.value);
  };

  const handleVmAdminSshKeyPublicKeyChange = (event) => {
    setVirtualMachineAdminSshKeyPublicKey(event.target.value);
  };

  const handleVmOsDiskCachineChange = (event) => {
    setVirtualMachineOsDiskCachine(event.target.value);
  };

  const handleVmOsDiskStorageAccountTypeChange = (event) => {
    setVirtualMachineOsDiskStorageAccountType(event.target.value);
  };

  const handleVmSourceImageReferenceOfferChange = (event) => {
    setVirtualMachineSourceImageReferenceOffer(event.target.value);
  };

  const handleVmSourceImageReferencePublisherChange = (event) => {
    setVirtualMachineSourceImageReferencePublisher(event.target.value);
  };

  const handleVmSourceImageReferenceSkuChange = (event) => {
    setVirtualMachineSourceImageReferenceSku(event.target.value);
  };

  const handleVmSourceImageReferenceVersionChange = (event) => {
    setVirtualMachineSourceImageReferenceVersion(event.target.value);
  };

  return (
    <>
      <h2>VirtualMachine</h2>

      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.name">
            <Form.Label>Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="name"
              value={virtualMachine.name}
              onChange={handleVmNameChange}
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.adminusername">
            <Form.Label>AdminUsername</Form.Label>
            <Form.Control
              type="text"
              placeholder="AdminUsername"
              value={virtualMachine.adminUsername}
              onChange={handleVmAdminUsernameChange}
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.size">
            <Form.Label>Size</Form.Label>
            <Form.Control
              type="text"
              placeholder="Size"
              value={virtualMachine.size}
              onChange={handleVmSizeChange}
            />
          </Form.Group>
        </Col>
      </Row>

      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.name">
            <Form.Label>ResourceGroupName</Form.Label>
            <Form.Control
              type="text"
              placeholder="ResourceGroupName"
              value={virtualMachine.resourceGroupName}
              disabled
            />
          </Form.Group>
        </Col>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.localName">
            <Form.Label>LocalName</Form.Label>
            <Form.Control
              type="text"
              placeholder="localName"
              value={virtualMachine.localName}
              disabled
            />
          </Form.Group>
        </Col>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.location">
            <Form.Label>Location</Form.Label>
            <Form.Control
              type="text"
              placeholder="location"
              value={virtualMachine.location}
              disabled
            />
          </Form.Group>
        </Col>
      </Row>

      <h5>SSH</h5>
      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.adminsshkeyusername">
            <Form.Label>AdminSshKeyUsername</Form.Label>
            <Form.Control
              type="text"
              placeholder="AdminSshKeyUsername"
              value={virtualMachine.adminSshKeyUsername}
              onChange={handleVmAdminSshKeyUsernameChange}
            />
          </Form.Group>
        </Col>

        <Col sm={8}>
          <Form.Group className="mb-3" controlId="form.vm.adminsshkeypublickey">
            <Form.Label>AdminSshKeyPublicKey</Form.Label>
            <Form.Control
              type="text"
              placeholder="AdminSshKeyPublicKey"
              value={virtualMachine.adminSshKeyPublicKey}
              onChange={handleVmAdminSshKeyPublicKeyChange}
            />
          </Form.Group>
        </Col>
      </Row>

      <h5>OsDisk</h5>
      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.vm.osdiskcachine">
            <Form.Label>OsDiskCachine</Form.Label>
            <Form.Control
              type="text"
              placeholder="OsDiskCachine"
              value={virtualMachine.osDiskCachine}
              onChange={handleVmOsDiskCachineChange}
            />
          </Form.Group>
        </Col>

        <Col sm={4}>
          <Form.Group
            className="mb-3"
            controlId="form.vm.osdiskstorageaccounttype"
          >
            <Form.Label>OsDiskStorageAccountType</Form.Label>
            <Form.Control
              type="text"
              placeholder="OsDiskStorageAccountType"
              value={virtualMachine.osDiskStorageAccountType}
              onChange={handleVmOsDiskStorageAccountTypeChange}
            />
          </Form.Group>
        </Col>
      </Row>

      <h5>Image</h5>

      <Row>
        <Col sm={3}>
          <Form.Group
            className="mb-3"
            controlId="form.vm.sourceimagereferenceoffer"
          >
            <Form.Label>SourceImageReferenceOffer</Form.Label>
            <Form.Control
              type="text"
              placeholder="SourceImageReferenceOffer"
              value={virtualMachine.sourceImageReferenceOffer}
              onChange={handleVmSourceImageReferenceOfferChange}
            />
          </Form.Group>
        </Col>

        <Col sm={3}>
          <Form.Group
            className="mb-3"
            controlId="form.vm.sourceimagereferencepublisher"
          >
            <Form.Label>SourceImageReferencePublisher</Form.Label>
            <Form.Control
              type="text"
              placeholder="SourceImageReferencePublisher"
              value={virtualMachine.sourceImageReferencePublisher}
              onChange={handleVmSourceImageReferencePublisherChange}
            />
          </Form.Group>
        </Col>

        <Col sm={3}>
          <Form.Group
            className="mb-3"
            controlId="form.vm.sourceimagereferencesku"
          >
            <Form.Label>SourceImageReferenceSku</Form.Label>
            <Form.Control
              type="text"
              placeholder="SourceImageReferenceSku"
              value={virtualMachine.sourceImageReferenceSku}
              onChange={handleVmSourceImageReferenceSkuChange}
            />
          </Form.Group>
        </Col>

        <Col sm={3}>
          <Form.Group
            className="mb-3"
            controlId="form.vm.sourceimagereferenceversion"
          >
            <Form.Label>SourceImageReferenceVersion</Form.Label>
            <Form.Control
              type="text"
              placeholder="SourceImageReferenceVersion"
              value={virtualMachine.sourceImageReferenceVersion}
              onChange={handleVmSourceImageReferenceVersionChange}
            />
          </Form.Group>
        </Col>
      </Row>
    </>
  );
};

FormVirtualMachine.propTypes = {};

FormVirtualMachine.defaultProps = {};

export default FormVirtualMachine;
