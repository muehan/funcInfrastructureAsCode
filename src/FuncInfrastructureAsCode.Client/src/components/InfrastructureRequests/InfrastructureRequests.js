import React from "react";
import Table from "react-bootstrap/Table";
import { useFetch } from "../../apis/FetchData";
import "./InfrastructureRequests.css";
import { Button } from "react-bootstrap";
import moment from "moment";
import Nav from "react-bootstrap/Nav";
import { Link } from "react-router-dom";

const InfrastructureRequests = () => {
  const requests = useFetch("/InfrastructureRequests");

  if (!Array.isArray(requests.response)) {
    return <p>Loading...</p>;
  }

  const data = requests.response;

  return (
    <div>
      <h3>Requests</h3>
      <Nav.Link as={Link} to={"/request/virtualMachines"}>
        <Button
          style={{ marginBottom: "5px" }}
          variant="primary"
          // href="/request/virtualMachines"
        >
          Request VM
        </Button>
      </Nav.Link>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>RowKey</th>
            <th>RequesterName</th>
            <th>RequesterEmail</th>
            <th>RequestStatus</th>
            <th>CreatedAt</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((row) => {
              return (
                <tr key={row.rowKey}>
                  <td>
                    <Nav.Link as={Link} to={"/request/details/" + row.rowKey}>
                      {row.rowKey}
                    </Nav.Link>
                  </td>
                  <td>{row.requesterName}</td>
                  <td>{row.requesterEmail}</td>
                  <td>{row.requestStatus}</td>
                  <td>{moment(row.createdAt).format("yyyy-MM-DD HH:mm")}</td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </div>
  );
};

InfrastructureRequests.propTypes = {};

InfrastructureRequests.defaultProps = {};

export default InfrastructureRequests;
