import { call } from './common';

export const GET_ME_SUCCEEDED = 'GET_ME_SUCCEEDED';

export function getMe() {
  return call(
    'get',
    GET_ME_SUCCEEDED,
    'user',
    null,
    'api/v1/me'
  );
}

