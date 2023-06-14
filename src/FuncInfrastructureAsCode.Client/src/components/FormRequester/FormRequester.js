import React from "react";
import { Form, Row, Col } from "react-bootstrap";
import { useVirtualMachine } from "../../providers/VirtualMachineProvider";
import "./FormRequester.css";

const FormRequester = () => {
  const { infrastructureRequest, setRequesterName, setRequesterEmail } =
    useVirtualMachine();

  const handleRequesterNameChanged = (event) => {
    setRequesterName(event.target.value);
  };

  const handleRequesterEmailChanged = (event) => {
    setRequesterEmail(event.target.value);
  };

  return (
    <>
      <h2>Requester</h2>
      <Row>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.req.reqname">
            <Form.Label>RequesterName</Form.Label>
            <Form.Control
              type="text"
              placeholder="RequesterName"
              value={infrastructureRequest.requesterName}
              onChange={handleRequesterNameChanged}
            />
          </Form.Group>
        </Col>
        <Col sm={4}>
          <Form.Group className="mb-3" controlId="form.req.reqemail">
            <Form.Label>RequesterEmail</Form.Label>
            <Form.Control
              type="text"
              placeholder="RequesterEmail"
              value={infrastructureRequest.requesterEmail}
              onChange={handleRequesterEmailChanged}
            />
          </Form.Group>
        </Col>
      </Row>
    </>
  );
};

FormRequester.propTypes = {};

FormRequester.defaultProps = {};

export default FormRequester;
