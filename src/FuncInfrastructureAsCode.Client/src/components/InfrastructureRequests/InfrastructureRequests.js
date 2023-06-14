import React from 'react';
import Table from 'react-bootstrap/Table';
import { useFetch } from '../../apis/FetchData';
import './InfrastructureRequests.css';
import { Button } from 'react-bootstrap';

const InfrastructureRequests = () => {

  const requests = useFetch("/InfrastructureRequests");

  if (!Array.isArray(requests.response)) {
    return <p>Loading...</p>;
  }

  const data = requests.response;

  return (
    <div>
      <h3>Requests</h3>
      <Button style={{marginBottom: '5px'}} variant="primary" href="/request/virtualMachines">Request VM</Button>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>IRowKey</th>
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
                  <td>{row.rowKey}</td>
                  <td>{row.requesterName}</td>
                  <td>{row.requesterEmail}</td>
                  <td>{row.requestStatus}</td>
                  <td>{row.createdAt}</td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </div>
  );
}

InfrastructureRequests.propTypes = {};

InfrastructureRequests.defaultProps = {};

export default InfrastructureRequests;
