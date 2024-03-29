import "./ResourceGroups.css";
import React from "react";
import Table from "react-bootstrap/Table";
import { useFetch } from "../../apis/FetchData";

const ResourceGroups = (props) => {
  const url = props.requestId
    ? "/ResourceGroups/" + props.requestId
    : "/ResourceGroups";

  const resourceGroups = useFetch(url);

  const data = resourceGroups.response;

  if (!Array.isArray(resourceGroups.response)) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h3>ResourceGroups</h3>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Name</th>
            <th>Localname</th>
            <th>Location</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((row) => {
              return (
                <tr key={row.name}>
                  <td>{row.name}</td>
                  <td>{row.localName}</td>
                  <td>{row.location}</td>
                  <td>{row.status}</td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </div>
  );
};

ResourceGroups.propTypes = {};

ResourceGroups.defaultProps = {};

export default ResourceGroups;
