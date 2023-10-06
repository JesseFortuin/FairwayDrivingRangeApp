export const bookingGuard = () => {
  if (sessionStorage.getItem('booking')) {
    return true;
  }

  return false;
}
