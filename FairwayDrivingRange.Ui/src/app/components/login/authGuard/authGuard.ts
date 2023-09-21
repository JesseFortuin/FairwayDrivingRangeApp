export const authGuard = () => {
  if (sessionStorage.getItem('Token')) {
    return true;
  }

  return false;
}
