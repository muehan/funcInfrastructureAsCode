import React, { useEffect } from "react";
import { useMsal, useAccount } from "@azure/msal-react";

export const useFetch = (url) => {
  const { instance, accounts } = useMsal();
  const account = useAccount(accounts[0] || {});

  const [response, setResponse] = React.useState({});
  const [error, setError] = React.useState({});

  var baseUrl = process.env.REACT_APP_FUNCTION_API;

  useEffect(() => {
    const fetchData = async () => {
      if (account) {
        instance
          .acquireTokenSilent({
            scopes: [
              "api://aaa69109-96f8-4597-b27c-335a6c506098/access_as_user",
            ],
            account: account,
          })
          .then((token) => {
            try {
              fetch(
                baseUrl + url,
                {
                  headers: {
                    Accept: "application/json",
                    Authorization: "Bearer " + token.accessToken,
                  },
                }
              )
                .then((response) => {
                  return response.json();
                })
                .then((data) => {
                  setResponse(data);
                });
            } catch (error) {
              setError(error);
            }
          });
      }
    };

    fetchData();
  }, [account, instance, url, baseUrl]);

  return { response, error };
};
