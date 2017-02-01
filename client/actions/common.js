import * as api from 'lib/api';

export function call(
  apiAction,
  successAction,
  successProperty,
  failedAction,
  url,
  params,
) {
  return (dispatch) => {
    const onSuccess = (response) => {
      dispatch({
        [successProperty]: response.data,
        type: successAction,
      });
    };
    const onError = (error) => {
      dispatch({
        error: error.statusText,
        type: failedAction,
      });
    };
    const promise = (api)[apiAction](url, params);
    if (promise) {
      return promise.then(onSuccess, onError);
    }
    return Promise.resolve();
  };
}
