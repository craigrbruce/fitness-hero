import call from './common';

export const SIGNOUT_SUCCEEDED = 'SIGNOUT_SUCCEEDED';
export const SIGNOUT_FAILED = 'SIGNOUT_FAILED';

export function signOut() {
  return call(
    'post',
    SIGNOUT_SUCCEEDED,
    'account',
    SIGNOUT_FAILED,
    'accounts/signout'
  );
}
