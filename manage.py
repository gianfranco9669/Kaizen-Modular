#!/usr/bin/env python
import os
import sys


def main() -> None:
    os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'kaizen.settings')
    try:
        from django.core.management import execute_from_command_line
    except ImportError as exc:
        raise ImportError(
            'No se pudo importar Django. Instalá dependencias antes de ejecutar manage.py.'
        ) from exc
    execute_from_command_line(sys.argv)


if __name__ == '__main__':
    main()
